$(document).ready(function ()
{

    dataTable = $('#DimStatusDetail').dataTable({
        serverSide: true,
        processing: true,
        //searchDelay: 500,
        pageLength: 10,
        infoFiltered: true,
        orderMulti: false,
        "ajax": {
            "url": "/api/Report/PostStatusDetail",
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            { "data": "id", "name": "id", "autoWidth": true },
            { "data": "name", "name": "name", "autoWidth": true },
            { "data": "statusId", "name": "statusId", "autoWidth": true },
            {
                "render": function (data, row) {
                    return "<a href='#' class='btn btn-danger' onclick=DeleteCustomer('" + row.statusId + "'); >جزییات</a>";
                }
            }
        ]
    });
});