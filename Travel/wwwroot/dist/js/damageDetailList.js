var dataTable;
$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#demoGrid2').DataTable({
        serverSide: true,
        //processing: true,
        //searchDelay: 500,
        //pageLength: 50,
        //infoFiltered: true,
        //orderMulti: true,
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
            "url": "/Report/IndexDetail/{}",
            "type": "GET",
            "datatype": "json"
        },

        "columnDefs":
            [{
                "targets": [14],
                "searchable": false,
                "orderable": false
            }],

        "columns": [
            { "data": "caseId", "title": "کد پرونده" },
            { "data": "companyId", "title": "کد شرکت"},
            { "data": "userType", "title": "نوع کاربر"},
            { "data": "city", "title": "کد شهر" },
            { "data": "state", "title": "کد استان"},
            { "data": "locationTypeId", "title": "نوع محل"},
            { "data": "locationStatusId", "title": "وضیعت محل" },
            { "data": "compensationTypeId", "title": "نوع" },
            { "data": "messageId", "title": "کد پیام"},
            { "data": "refId", "title": "کد مرجع"},
            { "data": "seen", "title": "وضعیت رویت"},
            { "data": "documentTypeId", "title": "کد فایل"},
            { "data": "fileName", "title": "نام فایل" },
            { "data": "fileType", "title": "نوع فایل" },
            { "data": "ImageAddress", "title": "مسیر عکس" },
            
        ],
        "width": "100%"
    });

}



