

(function ($) {
    'use strict';
    $(document).ready(function () {
        $.extend(true, $.fn.dataTable.defaults, {
            "language": {
                "sProcessing": "Đang xử lý...",
                "sLengthMenu": "Xem _MENU_ mục",
                "sZeroRecords": "Không tìm thấy dòng nào phù hợp",
                "sInfo": "Đang xem _START_ đến _END_ trong tổng số _TOTAL_ mục",
                "sInfoEmpty": "Đang xem 0 đến 0 trong tổng số 0 mục",
                "sInfoFiltered": "(được lọc từ _MAX_ mục)",
                "sInfoPostFix": "",
                "sSearch": "Tìm:",
                "sUrl": "",
                "oPaginate": {
                    "sFirst": "Đầu",
                    "sPrevious": "Trước",
                    "sNext": "Tiếp",
                    "sLast": "Cuối"
                }
            }
        });

        // nếu có thuộc tính is-development="true" thì sẽ hiển thị thông báo sử dụng sweetalert2
        $(document).on('click', '[is-development="true"]', function () {
            event.preventDefault();
            Swal.fire({
                title: 'Chức năng đang được phát triển',
                text: 'Chức năng này đang được phát triển, vui lòng quay lại sau',
                icon: 'info',
                confirmButtonText: 'Đã hiểu'
            });
        });
    });
})(jQuery);