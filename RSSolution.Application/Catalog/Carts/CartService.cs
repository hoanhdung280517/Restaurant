using Microsoft.EntityFrameworkCore;
using RSSolution.Data.EF;
using RSSolution.Data.Entities;
using RSSolution.ViewModels.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSolution.Application.Catalog.Carts
{
    public class CartService : ICartService
    {
        private readonly RSDbContext _context;
        public CartService(RSDbContext context)
        {
            _context = context;
        }

        public async Task<int> Add(CartRequest request)
        {
            if (request == null)
                return 0;
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            var userid = user.Id;      
            foreach (var item in request.CartItems)
            {
                var cart = new Cart(); 
                cart.MealId = item.MealId;
                cart.UserId = userid;
                cart.Price = item.Price;
                cart.Quantity = item.Quantity;
                cart.TableId = request.TableId;
                cart.DateCreated = DateTime.Now;
                _context.Add(cart);
            }      
            await _context.SaveChangesAsync();        
            return 1;
        }
        public async Task<List<CartViewModel>> GetCartById(int tableId)
        {
            var cart = await _context.Carts.Select(r => new Itemvm()
            {
                    Id = r.Id,
                    MealId = r.MealId,
                    UserId = r.UserId,
                    TableId= r.TableId,
                    Price = r.Price,
                    Quantity = r.Quantity,               
            }).ToListAsync();

            var cartitem = new List<CartViewModel>();
            foreach(var item in cart) 
            {
                if(item.TableId == tableId) 
                { 
                    var meal = _context.MealTranslations.FirstOrDefault(u => u.MealId == item.MealId);
                    var user = _context.Users.FirstOrDefault(u => u.Id == item.UserId);
                    var img = _context.MealImages.FirstOrDefault(i=>i.MealId == item.MealId);
                    var vm = new CartViewModel()
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        MealId = item.MealId,
                        Name = meal.Name,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Description = meal.Description,
                        Image = img.ImagePath,
                    };
                    cartitem.Add(vm);
                }
            }           
            return cartitem;
        }
        public async Task<int> ChangeTable(ChangeTableRequest request)
        {
            if (request == null)
                return 0;
            var user = _context.Users.FirstOrDefault(u => u.Email == request.Email);
            var userid = user.Id;
            var carttable = await _context.Carts.FirstOrDefaultAsync(x=>x.TableId == request.TableId);
            var cart = await _context.Carts.Select(r => new Itemvm()
            {
                Id = r.Id,
                MealId = r.MealId,
                UserId = r.UserId,
                TableId = r.TableId,
                Price = r.Price,
                Quantity = r.Quantity,
            }).ToListAsync();
            foreach ( var item in cart)
            {               
               if(item.UserId == userid)
               {
                    if (carttable == null)
                    {
                         var cartitem = await _context.Carts.FindAsync(item.Id);
                         cartitem.TableId = request.TableId;
                         cartitem.UserId = userid;
                         await _context.SaveChangesAsync();
                    }
                    else
                    {
                        return 0;
                    }                   
               }
            }            
            return 1;
        }
        public async Task<int> MergeTable (MergeTableRequest request)
        {
            if (request == null)
                return 0;
            var user = _context.Users.FirstOrDefault(u => u.UserName == request.UserName);
            var userid = user.Id;
            var carttable =  _context.Carts.FirstOrDefault(x=>x.TableId == request.TableId);
            var cart = await _context.Carts.Select(r => new Itemvm()
            {
                Id = r.Id,
                MealId = r.MealId,
                UserId = r.UserId,
                TableId = r.TableId,
                Price = r.Price,
                Quantity = r.Quantity,
            }).ToListAsync();
            foreach (var item in cart)
            {
                if (item.UserId == userid)
                {
                    var cartitem = await _context.Carts.FindAsync(item.Id);
                    cartitem.TableId = request.TableId;
                    cartitem.UserId = carttable.UserId;
                    await _context.SaveChangesAsync();
                }
            }
            return 1;
        }
        public async Task<int> DeleteCart(int tableId)
        {
            var cart = await _context.Carts.Select(r => new Itemvm()
            {
                Id = r.Id,
                MealId = r.MealId,
                UserId = r.UserId,
                TableId = r.TableId,
                Price = r.Price,
                Quantity = r.Quantity,
            }).ToListAsync();
            foreach (var item in cart)
            {
                if (item.TableId == tableId)
                {
                    var cartdl = await _context.Carts.FindAsync(item.Id);
                    _context.Carts.Remove(cartdl);
                }
            }
            return await _context.SaveChangesAsync();
        }
    }
}
