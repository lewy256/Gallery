let timeout = null;
document.getElementById('livesearchtags').addEventListener('keyup', function (e) {
    clearTimeout(timeout);
    timeout = setTimeout(function () {
        LiveSearch()
    }, 800);
});

function LiveSearch() {
    let value = document.getElementById('livesearchtags').value

    $.ajax({
        type: "POST",
        url: "/Gallery/Index",
        data: { searchString: value },
        datatype: "html",
        success: function (data) {
            $('#result').html(data);
        }
    });
}