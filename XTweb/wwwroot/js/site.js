const body = document.querySelector("body"),
    slidebar = body.querySelector(".slidebar"),
    toogle = body.querySelector(".toogle"),
    admin_nav = body.querySelector(".admin-nav");

    toogle.addEventListener("click", () => {
        slidebar.classList.toggle("close"); 
        admin_nav.classList.toogle("close");
    });