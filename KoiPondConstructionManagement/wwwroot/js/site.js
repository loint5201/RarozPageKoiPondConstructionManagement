

(function ($) {
    'use strict';
    async function callAjax(url, method, data, headers, options) {
        return await $.ajax({
            url: url,
            method: method,
            data: data,
            headers: {
                ...headers
            },
            ...options
        });
    }
    
    $(document).ready(function () {
        $.ajaxSetup({
            headers: {
                'RequestVerificationToken': getToken()
            }
        });
        
        $.callAjax = callAjax;
        $.getToken = getToken;

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

        let loaderHtml = `<div id="loader" class="loader-wrapper">
                            <div class="loader"></div>
                        </div>`;
        
        $('body').append(loaderHtml);
        
        $.mask = () => {
            $('body').append(loaderHtml);
        }
        
        $.unmask = () => {
            $('#loader').remove();
        }

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

    document.addEventListener('DOMContentLoaded', function () {
        $('#loader').fadeOut(); // Ẩn loader khi DOM đã sẵn sàng
    });

    window.onerror = function(message, source, lineno, colno, error) {
        $('#loader').fadeOut(); // Ẩn loader trong trường hợp có lỗi
    };
})(jQuery);