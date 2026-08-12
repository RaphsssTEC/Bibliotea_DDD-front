document.addEventListener("DOMContentLoaded", () => {
    document.querySelectorAll(".alert").forEach((alert) => {
        setTimeout(() => {
            alert.style.opacity = "0";
            alert.style.transition = "opacity .3s ease";
            setTimeout(() => alert.remove(), 300);
        }, 4000);
    });
});
