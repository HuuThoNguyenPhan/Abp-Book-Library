$(function () {
    var l = abp.localization.getResource('BookLibrary');
    var createModal = new abp.ModalManager(abp.appPath + 'MemberCards/CreateModal');
    var editModal = new abp.ModalManager(abp.appPath + 'MemberCards/EditModal');

    var dataTable = $('#MemberCardsTable').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(acme.bookLibrary.memberCards.memberCard.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l(
                                            'MemberCardDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        acme.bookLibrary.memberCards.memberCard
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                },
                            ]
                    }
                },
                {
                    title: l('Name'),
                    data: "name"
                },
                {
                    title: l('Phone'),
                    data: "phone"
                },
                {
                    title: l('Email'),
                    data: "email"
                },
                {
                    title: l('Address'),
                    data: "address"
                },
                {
                    title: l('Exp Time'),
                    data: "expDate",
                    render: function (data) {
                        return luxon
                            .DateTime
                            .fromISO(data, {
                                locale: abp.localization.currentCulture.name
                            }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                    }
                }
            ]
        })
    );
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });
    editModal.onResult(function () {
        dataTable.ajax.reload();
    });
    $('#NewCardButton').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});