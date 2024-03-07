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

function deleteTone(){
    const deleteTone = document.getElementById("delete-tone")
    const toneId = deleteTone.getAttribute("tone-id")
    const href = "/delete/" + toneId;
    document.getElementById("modal-delete-btn").href = 
        `${href}`;
}