new DataTable('#tonesTable', {
    language: {
        lengthMenu: '_MENU_',
    },
});

function checkForm(){

    if(!document.getElementById("g-recaptcha-response").value){
        document.getElementById("reCaptcha-error").innerHTML =
            "Please verify that you're not a robot!";
        return false;
    }else{
        document.getElementById("submit-btn").type = "submit";
        return true;
    }
}