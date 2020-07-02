(function ($) {
    var _cardService = abp.services.app.card,
        l = abp.localization.getSource('Mahjong'),
        _$modal = $('#CardCreateModal'),
        _$form = _$modal.find('form'),
        _$table = $('#CardsTable');

    var _$cardsTable = _$table.DataTable({
        paging: true,
        serverSide: true,
        ajax: function (data, callback, settings) {
            var filter = $('#CardsSearchForm').serializeFormToObject(true);
            filter.maxResultCount = data.length;
            filter.skipCount = data.start;

            abp.ui.setBusy(_$table);
            _cardService.getAll(filter).done(function (result) {
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
                action: () => _$cardsTable.draw(false)
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
                data: 'cardType',
                sortable: true
            },
            {
                targets: 3,
                data: 'description',
                sortable: true
            },
            {
                targets: 4,
                data: 'total',
                sortable: true
            },
            {
                targets: 5,
                data: 'commission',
                sortable: true
            },
            {
                targets: 6,
                data: null,
                sortable: false,
                autoWidth: false,
                defaultContent: '',
                render: (data, type, row, meta) => {
                    return [
                        `   <button type="button" class="btn btn-sm bg-secondary edit-card" data-card-id="${row.id}" data-toggle="modal" data-target="#CardEditModal">`,
                        `       <i class="fas fa-pencil-alt"></i> ${l('Edit')}`,
                        '   </button>',
                        `   <button type="button" class="btn btn-sm bg-danger edit-card delete-card" data-card-id="${row.id}" data-card-name="${row.name}">`,
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

        var card = _$form.serializeFormToObject();
        card.roleNames = [];
        var _$roleCheckboxes = $("input[name='role']:checked");
        if (_$roleCheckboxes) {
            for (var roleIndex = 0; roleIndex < _$roleCheckboxes.length; roleIndex++) {
                var _$roleCheckbox = $(_$roleCheckboxes[roleIndex]);
                card.roleNames.push(_$roleCheckbox.val());
            }
        }

        abp.ui.setBusy(_$modal);
        _cardService.create(card).done(function () {
            _$modal.modal('hide');
            _$form[0].reset();
            abp.notify.info(l('SavedSuccessfully'));
            _$cardsTable.ajax.reload();
        }).always(function () {
            abp.ui.clearBusy(_$modal);
        });
    });

    $(document).on('click', '.delete-card', function () {
        var cardId = $(this).attr("data-card-id");
        var cardName = $(this).attr('data-card-name');

        deleteCard(cardId, cardName);
    });

    function deleteCard(cardId, cardName) {
        abp.message.confirm(
            abp.utils.formatString(
                l('AreYouSureWantToDelete'),
                cardName),
            null,
            (isConfirmed) => {
                if (isConfirmed) {
                    _cardService.delete({
                        id: cardId
                    }).done(() => {
                        abp.notify.info(l('SuccessfullyDeleted'));
                        _$cardsTable.ajax.reload();
                    });
                }
            }
        );
    }

    $(document).on('click', '.edit-card', function (e) {
        var cardId = $(this).attr("data-card-id");

        e.preventDefault();
        abp.ajax({
            url: abp.appPath + 'Cards/EditModal?cardId=' + cardId,
            type: 'POST',
            dataType: 'html',
            success: function (content) {
                $('#CardEditModal div.modal-content').html(content);
            },
            error: function (e) { }
        });
    });

    $(document).on('click', 'a[data-target="#CardCreateModal"]', (e) => {
        $('.nav-tabs a[href="#card-details"]').tab('show')
    });

    abp.event.on('card.edited', (data) => {
        _$cardsTable.ajax.reload();
    });

    _$modal.on('shown.bs.modal', () => {
        _$modal.find('input:not([type=hidden]):first').focus();
    }).on('hidden.bs.modal', () => {
        _$form.clearForm();
    });

    $('.btn-search').on('click', (e) => {
        _$cardsTable.ajax.reload();
    });

    $('.txt-search').on('keypress', (e) => {
        if (e.which == 13) {
            _$cardsTable.ajax.reload();
            return false;
        }
    });
})(jQuery);
