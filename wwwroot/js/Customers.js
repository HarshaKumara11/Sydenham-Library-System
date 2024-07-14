$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblcustomer').DataTable({
        "ajax": { url: '/Customers/GetCustomers'},
        columns: [
            {
                data: 'id',
                render: function (data) {
                    return `<div>
                    <a href="Customers/Edit?id=${data}"><i class="fa fa-pencil-square-o"></i>Edit<a/> |
                    <a href="Customers/Details?id=${data}"><i class="fa fa-external-link"></i>View<a/>
                    </div>`
                }
            },
            { data: 'custID' },
            { data: 'fName' },
            { data: 'lName' },
            { data: 'email' },
            { data: 'phoneNumber1' },
            { data: 'phoneNumber2' },
            { data: 'address1'},
            { data: 'address2' },
            { data: 'city' },
            { data: 'postalCode' },
            { data: 'nic' },
            { data: 'examYear' },
            { data: 'dateJoined'},
            {
                data: 'dateCreated',
                render: function (data, type, row) {
                    if (data) {
                        var date = new Date(data);
                        var day = ("0" + date.getDate()).slice(-2);
                        var month = ("0" + (date.getMonth() + 1)).slice(-2);
                        var year = date.getFullYear();
                        return year + "-" + month + "-" + day;
                    }
                    return "";
                }
            }

        ],
        scrollX: true
        

    });
}



