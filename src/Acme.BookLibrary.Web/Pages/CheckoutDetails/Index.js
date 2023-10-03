$(function () {
    var l = abp.localization.getResource('BookLibrary');
    var createModal = new abp.ModalManager(abp.appPath + 'CheckoutDetails/CreateModal');

    var dataTable = $('#CheckoutDetails').DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(acme.bookLibrary.checkouts.checkout.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('View'),
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
                                },
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
                                            'BookDeletionConfirmationMessage',
                                            data.record.name
                                        );
                                    },
                                    action: function (data) {
                                        acme.bookLibrary.checkouts.checkout
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(
                                                    l('SuccessfullyDeleted')
                                                );
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('Id'),
                    data: "deposit"
                },
                {
                    title: l('Name'),
                    data: "deposit"
                },
                {
                    title: l('Return time'),
                    data: "isFinished"
                },
                {
                    title: l('Status'),
                }
            ]
        })
    );
    $('#NewCheckoutDetail').click(function (e) {
        e.preventDefault();
        createModal.open();
    });
});