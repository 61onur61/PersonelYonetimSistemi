// Call the dataTables jQuery plugin
$(document).ready(function() {
    $('.dataTable').DataTable({
        "language": {
            "lengthMenu": "Girdi Sayisi _MENU_",
            "zeroRecords": "Tabloda Hicbir Girdi Yok...",
            "info": "_TOTAL_ tane girdiden _START_-_END_ arasi gosteriliyor...",
            "infoEmpty": "0 tane girdiden 0-0 arasi gosteriliyor...",
            "infoFiltered": "aaa",
            "sSearch": "Arama Yap!",
            "oPaginate": {
                "sFirst": "ileri",
                "sLast": "geri",
                "sNext": "ileri",
                "sPrevious": "geri"
            },
            "sProcessing": "Yukleniyor..."
        }
    });
});
