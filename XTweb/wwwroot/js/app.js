const scroller = document.querySelectorAll(".scroller");

if (!window.matchMedia("(prefers-reduced-motion:reduce)").matches) {
    addAnimation();
}
function addAnimation() {
    scroller.forEach(scroller => {
        scroller.setAttribute("data-animated", true)

        const card = scroller.querySelector('.card');
        const scrollerContent = Array.from(card.children);

        scrollerContent.forEach(item => {
            const duplicatedItem = item.cloneNode(true);
            duplicatedItem.setAttribute('aria-hidden', true)
            scrollerContent.appendChild(duplicatedItem);
        })
    });
}
