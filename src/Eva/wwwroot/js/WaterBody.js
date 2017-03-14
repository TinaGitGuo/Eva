$(document).ready(function () {
    $("#site-target").on("change", function () {
        //alert("started")
        $list = $("#wb-target");       
        $.ajax({
            type: "GET",
            url: "/Equipments/GetWaterBody",   // I add a "/ ,it works too                 
        data: { id: $("#site-target").val() }, //id of the site which is used to extract waterbodies
        traditional: true,
        success: function (result) {
            //alert("Empty List");
            $list.empty();
            $.each(result, function (i, item) {
                //alert(item);
                //$list.append('<option value="' + item["WaterBodyId"] + '"> ' + item["WBName"] + ' </option>');
                $list.append('<option value="' + item["DivisionID"] + '"> ' + item["iDepartment"] + ' </option>');
            });
        },
        error: function(XMLHttpRequest, textStatus, errorThrown) {
            alert(XMLHttpRequest.status);
            alert(XMLHttpRequest.readyState);
            alert(textStatus);
        },
          
            //alert("Danger!! Will Robertson Danger!!!!!!!!!!!");

        }
    );
});
});