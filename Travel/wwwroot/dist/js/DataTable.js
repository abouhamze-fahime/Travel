$(document).ready(function () {
    $('#MyTable thead tr')
        .clone(true)
        .addClass('filters')
        .appendTo('#MyTable thead');


    $('#MyTable').DataTable({
        "scrollY": "450px",
        "scrollCollapse": true,
        "paging": true,
        "orderCellsTop": true,
        "fixedHeader": true,
        "processing": true, //for show processing bar 
        // "serverSide": true, //for process on server side 
        "filter": true,
        "pageLength": 20,
        "orderMulti": false, // for disable multi column order



        "language": {
            "decimal": "",
            "emptyTable": "هیچ اطلاعاتی در دسترس نیست",
            "info": "نمایش START تا END از TOTAL رکورد",
            "infoEmpty": "نمایش 0 تا 0 از 0 رکورد",
            "infoFiltered": "(فیلتر شده از MAX همه اطلاعات)",
            "infoPostFix": "",
            "thousands": ",",
            "lengthMenu": " تعداد در صفحه MENU ",
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
        initComplete: function () {
            var api = this.api();

            // For each column
            api
                .columns()
                .eq(0)
                .each(function (colIdx) {
                    // Set the header cell to contain the input element
                    var cell = $('.filters th').eq(
                        $(api.column(colIdx).header()).index()
                    );
                    var title = $(cell).text();
                    $(cell).html('<input type="text" placeholder="' + title + '" />');

                    if ($(api.column(colIdx).header()).index() >= 0) {
                        $(cell).html('<input type="text" placeholder="' + title + '"/>');
                    }

                    // On every keypress in this input
                    $(
                        'input',
                        $('.filters th').eq($(api.column(colIdx).header()).index())
                    )
                        .off('keyup change')
                        .on('keyup change', function (e) {
                            e.stopPropagation();

                            // Get the search value
                            $(this).attr('title', $(this).val());
                            var regexr = '({search})'; //$(this).parents('th').find('select').val();

                            var cursorPosition = this.selectionStart;
                            // Search the column for that value
                            api
                                .column(colIdx)
                                .search(
                                    this.value != ''
                                        ? regexr.replace('{search}', '(((' + this.value + ')))')
                                        : '',
                                    this.value != '',
                                    this.value == ''
                                )
                                .draw();

                            $(this)
                                .focus()[0]
                                .setSelectionRange(cursorPosition, cursorPosition);
                        });
                });
        },
    });
});