$(document).ready(function () {
    const id = $('#id').val();
    const culture = $('#hidCulture').val();
    $.ajax({
        type: "POST",
        url: "/" + culture + '/Meal/GetComment',
        data: {
            id: id,
        },
        success: function (res) {
            var listComment = `<ul id='list-comment' data-role='listview' style='list-style-type: none; margin-left:0;'>`;
            for (let comment of res) {
                listComment += `<li style=\"background-color: white; border-bottom: 1px solid gray; margin-bottom: 1rem; border-left: 1px solid gray;\">
                                    <p style=\"text-transform: capitalize; padding-top: 5px; padding-left:10px;"\>Account: ${comment.userName} <small>(${comment.dateCreate})</small></p>  
                                    <p style=\"padding-left:10px;"\>Comment: ${comment.comments}</p>
                                </li>`;
            }
            listComment += `</ul>`;

            // Add list to UI.
            $('#list-comment').empty().append(listComment);
        },
        error: function (err) {
            console.log(err);
        }
    });
});

$(document).on('submit', '#Createcomment', create);
function create(e) {
    e.preventDefault()
    const id = $('#id').val();
    const comment = $('#comments').val();
    const culture = $('#hidCulture').val();
    $.ajax({
        type: "POST",
        url: "/" + culture + '/Meal/CreateComment',
        data: {
            id: id,
            comment : comment,
        },
        success: function (res) {
            alert('Comment successfully !!!');
            window.location.reload();

        },
        error: function (err) {
            console.log(err);
        }
    });

}