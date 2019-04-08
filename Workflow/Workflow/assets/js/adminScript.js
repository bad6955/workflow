/**
 * ******* ADMIN JS
 */
function AdminPageLoaded() {
    $('.menu .item')
        .tab()
        ;
    $('.ui.dropdown')
        .dropdown()
        ;
}

function saveSelection() {
    var roleEle = document.getElementById("RoleSelect");
    var role = roleEle.options[roleEle.selectedIndex].value;

    var companyEle = document.getElementById("CompanySelect");
    var company = companyEle.options[companyEle.selectedIndex].value;

    var accountEle = document.getElementById("LockedAccountSelect");
    var account = accountEle.options[accountEle.selectedIndex].value;

    document.getElementById("SelectedRole").value = role;
    document.getElementById("SelectedCompany").value = company;
    document.getElementById("SelectedAccount").value = account;

    /* if selected role is not a Client, default company to VC */
    if (role !== 1) {
        document.getElementById("CompanySelect").value = 1;
    }
}
function DisplayDeleteUser() {
    $('.ui.tiny.delete.modal').modal('show');
}

function EditUser(fname, lname, rolename, roleid, userid) {
    $('.ui.small.test.modal').modal('show');

    document.getElementById("user_firstname").value = fname;
    document.getElementById("user_lastname").value = lname;
    document.getElementById("UserRole").value = roleid;
    document.getElementById("UserID").value = userid;
    document.getElementById("UserSelectedRole").value = roleid;
}

function saveUser() {
    var roleEle = document.getElementById("UserRole");
    var role = roleEle.options[roleEle.selectedIndex].value;

    document.getElementById("UserSelectedRole").value = role;
    document.getElementById("UserFirstName").value = document.getElementById("user_firstname").value;
    document.getElementById("UserLastName").value = document.getElementById("user_lastname").value;
}
