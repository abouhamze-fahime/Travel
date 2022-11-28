$(document).ready(function ()
{

    dataTable = $('#DimStatus').dataTable({
        serverSide: true,
        processing: true,
        //searchDelay: 500,
        pageLength: 10,
        infoFiltered: true,
        orderMulti: false,
        "ajax": {
            "url": "/api/Report/GetPolicyState",
            "type": "POST",
            "datatype": "json"
        },

        "columns": [
            { "data": "code", "name": "code", "autoWidth": true },
            { "data": "title", "name": "title", "autoWidth": true },
            { "data": "pId", "name": "pId", "autoWidth": true },
            {
                "data" :"pId" , 
                "render": function (data, row) {
                    return `<a   

                 href="/api/Report/GetStatusDetail"  class "btn btn-success text-white" style='cursor:pointer'; asp-controller="Report" asp-action="PostStatusDetail"  asp-route-pid="${data}"
                             
                   >جزییات</a>`;
                }
                 //   ? id = ${ data }"

            }
        ]
    });
});