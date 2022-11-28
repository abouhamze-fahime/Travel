
/* Formatting function for row details - modify as you need */
function format(d) {
    // `d` is the original data object for the row
    return '<table cellpadding="5" cellspacing="0" border="0" style="padding-left:50px;">' +
        '<tr>' +
        '<td>city:</td>' +
        '<td>' + d.city + '</td>' +
        '</tr>' +
        '<tr>' +
        '<td>state:</td>' +
        '<td>' + d.state + '</td>' +
        '</tr>' +
        '<tr>' +
        '<td>Extra info:</td>' +
        '<td>And any further details here (images etc)...</td>' +
        '</tr>' +
        '</table>';
}






var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {

    // Add event listener for opening and closing details
    $('#demoGrid tbody').on('click', 'td.details-control', function () {
        var tr = $(this).closest('tr');
        var row = table.row(tr);

        if (row.child.isShown()) {
            // This row is already open - close it
            row.child.hide();
            tr.removeClass('shown');
        }
        else {
            // Open this row
            row.child(format(row.data())).show();
            tr.addClass('shown');
        }
    });


    var columnDefs1 = [{
        "className": 'details-control',
        "orderable": false,
        "data": null,
        "defaultContent": '',
        "visible": true
    }]

    dataTable = $('#demoGrid').DataTable({
        serverSide: true,
        processing: true,
        //searchDelay: 500,
        pageLength: 10,
        infoFiltered: true,
        orderMulti: false,
        //scrollX: false,
        //  scrollY: true,
        columns: columnDefs1,
        fixedColumns: {
            left: 1,
            right: 1
        },
        "language": {
            "decimal": "",
            "emptyTable": "هیچ اطلاعاتی در دسترس نیست",
            "info": "نمایش _START_ تا _END_ از _TOTAL_ رکورد",
            "infoEmpty": "نمایش 0 تا 0 از 0 رکورد",
            "infoFiltered": "(فیلتر شده از _MAX_ همه اطلاعات)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": " تعداد در صفحه _MENU_ ",
            "loadingRecords": "در حال بارگذاری...",
            "processing": "در حال پردازش...",
            "search": "جستجو : ",
            "zeroRecords": "هیچ اطلاعاتی یافت نشد",
            "paginate": {
                "first": "اولین",
                "last": "آخرین",
                "next": "بعدی",
                "previous": "قبلی"
            },
            "aria": {
                "sortAscending": " : فعال سازی مرتب سازی ستون به صورت صعودی",
                "sortDescending": " : فعال سازی مرتب سازی ستون به صورت نزولی"
            }
        },
        "ajax": {
            "url": "/Report/DataList",
            "type": "POST",
            "datatype": "json"
        },
        "columnDefs":
            [{
                "targets": [14],
                "searchable": false,
                "orderable": false
            }],

        "columns": [
            { "data": "caseId", "title": "کد پرونده", "name": "caseId", "autoWidth": true },
            { "data": "trackingId", "title": "کد پیگیری", "name": "trackingId", "autoWidth": true },
            { "data": "billId", "title": "شماره چک", "name": "billId", "autoWidth": true },
            { "data": "nationalId", "title": "کد ملی", "name": "nationalId", "autoWidth": true },
            { "data": "userName", "title": "نام کاربر", "name": "userName", "autoWidth": true },
            { "data": "stateName", "title": "نام استان", "name": "stateName", "autoWidth": true },
            { "data": "cityName", "title": "نام شهر", "name": "cityName", "autoWidth": true },
            { "data": "mobileNo", "title": "شماره موبایل", "name": "mobileNo", "autoWidth": true },
            { "data": "postalCode", "title": "کد پستی", "name": "postalCode", "autoWidth": true },
            { "data": "amount", "title": "مبلغ", "name": "amount", "autoWidth": true },
            { "data": "descr", "title": "توضیحات", "name": "descr", "autoWidth": true },
            { "data": "eventDate", "title": "تاریخ حادثه", "name": "eventDate", "autoWidth": true },
            { "data": "eventTime", "title": "زمان حادثه", "name": "eventTime", "autoWidth": true },
            { "data": "message", "title": "پیام", "name": "message", "autoWidth": true },
            {
                "data": "caseId",
                "render": function (data, type, full, meta) {
                    return `<div class="text-center">
                           <a asp-controller="Report" asp-action="IndexDetail"  asp-route-id="${data}"   href="/Report/IndexDetail?id=${data}" class 'btn btn-success text-white' style='cursor:pointer; width:100px;'>
                           جزییات
                           </a>
                         </div> `;
                }
            }
        ]


    });

}








