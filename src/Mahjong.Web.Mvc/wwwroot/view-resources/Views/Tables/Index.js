(function ($) {
    var _tableService = abp.services.app.table,
        l = abp.localization.getSource('Mahjong'),
        _$modal = $('#TableCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#TablesTable');

    var _$tablesTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = $('#TablesSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;

            abp.ui.setBusy(_$table);
            _tableService.getAllWithSeats(filter).done(function (result) {
                callback({
                    recordsTotal: result.totalCount,
                    recordsFiltered: result.totalCount,
                    data: result.items
                });
            }).always(function () {
                abp.ui.clearBusy(_$table);
            });
        },
        buttons: [
            {
                name: 'refresh',
                text: '<i class="fas fa-redo-alt"></i>',
                action: () => _$tablesTable.draw(false)
            }
        ],
        responsive: {
            details: {
                type: 'column'
            }
        },
        columnDefs: [
            {
                targets: 0,
                className: 'control',
                defaultContent: '',
            },
            {
                targets: 1,
                data: 'id',
                sortable: true
            },
            {
                targets: 2,
                data: 'name',
                sortable: false
            },
            {
                targets: 3,
                data: 'description',
                sortable: false
            },
            {
                targets: 4,
                data: 'minAmount',
                sortable: false
            },
            {
                targets: 5,
                data: 'maxAmount',
                sortable: false
            },
            {
                targets: 6,
                data: 'unpaidCommissionTotal',
                sortable: false
            },
            {
                targets: 7,
                data: 'commissionRate',
                sortable: false
            },
            {
                targets: 8,
                data: 'round',
                sortable: false
            },
            {
                targets: 9,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary view-table-seats" data-table-id="${row.id}" data-toggle="modal" data-target="#TableSeatsModal">`,
                        `       <i class="fas fa-list"></i> ${l('Seats')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-secondary view-table-history" data-table-id="${row.id}" data-toggle="modal" data-target="#TableHistoryModal">`,
                        `       <i class="fas fa-list"></i> ${l('History')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-secondary edit-table" data-table-id="${row.id}" data-toggle="modal" data-target="#TableEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger edit-table delete-table" data-table-id="${row.id}" data-table-name="${row.name}">`,
                        `       <i class="fas fa-trash"></i> ${l('Delete')}`,
                        '   </button>'
                    ].join('');
                }
            }
        ]
    });

    _$form.validate({
        rules: {
            Password: "required",
            ConfirmPassword: {
                equalTo: "#Password"
            }
        }
    });

    _$form.find('.save-button').on('click', (e) => {
        e.preventDefault();

        if (!_$form.valid()) {
            return;
        }

        var table = _$form.serializeFormToObject();
        table.roleNames = [];
        var _$roleCheckboxes = $("input[name='role']:checked");
        if (_$roleCheckboxes) {
            for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
                var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
                table.roleNames.push(_$roleCheckbox.val());
            }
        }

        abp.ui.setBusy(_$modal);
        _tableService.create(table).done(function () {
            _$modal.modal('hide');
            _$form[0].reset();
            abp.notify.info(l('SavedSuccessfully'));
            _$tablesTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    });

    $(document).on('click', '.delete-table', function () {
        var tableId = $(this).attr("data-table-id");
        var tableName = $(this).attr('data-table-name');

        deleteTable(tableId, tableName);
    });

    function deleteTable(tableId, tableName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                tableName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _tableService.delete({
                        id: tableId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$tablesTable.ajax.reload();
                    });
                }
            }
        );
    }

    $(document).on('click', '.edit-table', function (e) {
        var tableId = $(this).attr("data-table-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Tables/EditModal?tableId=' + tableId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#TableEditModal div.modal-content').html(content);
            },
            error: function (e) { }
        });
    });

    $(document).on('click', '.view-table-history', function (e) {
        var tableId = $(this).attr("data-table-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Tables/HistoryModal?tableId=' + tableId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#TableHistoryModal div.modal-content').html(content);
            },
            error: function (e) { }
        });
    });

    $(document).on('click', '.view-table-seats', function (e) {
        var tableId = $(this).attr("data-table-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Tables/TableSeatsModal?tableId=' + tableId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#TableSeatsModal div.modal-content').html(content);
            },
            error: function (e) { }
        });
    });

    $(document).on('click', 'a[data-target="#TableCreateModal"]', (e) => {
        $('.nav-tabs a[href="#table-details"]').tab('show')
    });

    abp.event.on('table.edited', (data) => {
        _$tablesTable.ajax.reload();
    });

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$tablesTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$tablesTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
