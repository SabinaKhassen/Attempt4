<script src="http://code.jquery.com/jquery-3.1.1.min.js"></script>
    <script type="text/javascript">
        function Refresh() {
            $.ajax({
                url: '@Url.Action("Index")',
                method: "GET",
                success: function (data) {
                    $("#list").html(data);
                }
            })
        }
    
    function EditView() {
            $.ajax({
                url: '@Url.Action("Edit")',
                method: 'GET',
                success: function (data) {
                    $("#createOrEdit").html(data);
                }
            })
        }

//        function SaveEdit() {
//            console.log("save edit")
//        var id = $("#Id").val();
//        var firstName = $("#FirstName").val();
//        var lastName = $("#LastName").val();
//        console.log(id)
//        console.log(firstName)
//        console.log(lastName)
//        $.ajax({
//            url: '@Url.Action("Edit")',
//        method: 'POST',
//            data: JSON.stringify({Id: id, FirstName: firstName, LastName: lastName }),
//        dataType: 'json',
//        contentType: 'application/json; charset=utf-8',
//            success: function (data) {
//            $("#list").html(data);
//        $('#modal').modal("hide");
//    }
//})
}



    $(".createButton").click(() => {
            $.ajax({
                url: '@Url.Action("Edit")',
                method: 'GET',
                success: function (data) {
                    $("#createOrEdit").html(data);
                    $("#modal").modal("show");
                }
            })
        })
    
    function EditButton (idEdit) {
            console.log(idEdit)
        $.ajax({
            url: '@Url.Action("Edit")',
        method: 'GET',
            data: {id: idEdit},
            success: function (data) {
                $("#createOrEdit").html(data);
                $("#modal").modal("show");
            }
        })
    }
</script>