// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



document.addEventListener("DOMContentLoaded", function () {
    const cards = document.querySelectorAll(".card");

    var tooltipTriggerList = [].slice.call(document.querySelectorAll('[data-bs-toggle="tooltip"]'));
    var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
        return new bootstrap.Tooltip(tooltipTriggerEl);
    });

    cards.forEach(card => {
        card.addEventListener("click", function () {
            const tableNumber = this.getAttribute("data-table");
            const date = this.hasAttribute("data-date") ? this.getAttribute("data-date") : "N/A";
            const shift = this.hasAttribute("data-shift") ? this.getAttribute("data-shift") : "N/A";
            const maxCapacity = this.getAttribute("data-maxCapacity");
            const locationX = this.getAttribute("data-location-X");
            const locationY = this.getAttribute("data-location-Y");

            document.getElementById("modalTableNumber").textContent = tableNumber;
            if (document.getElementById("modalDate")) {
                document.getElementById("modalDate").textContent = date;
            }
            if (document.getElementById("modalShift")) {
                document.getElementById("modalShift").textContent = shift;
            }
            document.getElementById("modalMaxCapacity").textContent = maxCapacity
            if (document.getElementById("modalLocationX")) {
                document.getElementById("modalLocationX").textContent = locationX;
            }

            if (document.getElementById("modalLocationY")) {
                document.getElementById("modalLocationY").textContent = locationY;
            }

        
            document.getElementById("hiddenTableNumber").value = tableNumber;
            document.getElementById("hiddenMaxCapacity").value = maxCapacity;
            document.getElementById("hiddenDate").value = date;
            document.getElementById("hiddenShift").value = shift;

        });
    });

});
document.addEventListener("DOMContentLoaded", function () {
    const container = document.getElementById("container");
    const cards = document.querySelectorAll(".mesita");

    cards.forEach((card) => {
        card.addEventListener("dragstart", (e) => e.preventDefault()); 
        card.addEventListener("mousedown", startDrag);
    });

    let activeCard = null;
    let startX, startY, initialX, initialY;

    function startDrag(e) {
        e.preventDefault(); 
        activeCard = e.target;
        startX = e.clientX;
        startY = e.clientY;
        initialX = activeCard.offsetLeft;
        initialY = activeCard.offsetTop;

        activeCard.style.cursor = "grabbing";

        document.addEventListener("mousemove", moveCard);
        document.addEventListener("mouseup", stopDrag);
    }

    function moveCard(e) {
        if (!activeCard) return;

        let newX = initialX + (e.clientX - startX);
        let newY = initialY + (e.clientY - startY);

        let maxX = container.clientWidth - activeCard.clientWidth;
        let maxY = container.clientHeight - activeCard.clientHeight;

        newX = Math.max(0, Math.min(newX, maxX));
        newY = Math.max(0, Math.min(newY, maxY));

        activeCard.style.left = `${newX}px`;
        activeCard.style.top = `${newY}px`;
    }

    function stopDrag() {
        if (activeCard) {
            activeCard.style.cursor = "grab";
            document.removeEventListener("mousemove", moveCard);
            document.removeEventListener("mouseup", stopDrag);

            const Idlocate = activeCard.dataset.locatid;
            const tableNumber = activeCard.dataset.table;
            const x = activeCard.style.left;
            const y = activeCard.style.top;

            updateTablePosition(Idlocate, tableNumber, x, y);
        }
        activeCard = null;
    }

    async function updateTablePosition(Idlocate, tableNumber, x, y) {
        const requestData = {
            Id: parseInt(Idlocate),
            TableNumber: parseInt(tableNumber),
            CoordinateX: x,
            CoordinateY: y
        };

        await fetch("/UserTables/UpdateTablePosition", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(requestData)
        });
    }
});

document.addEventListener("DOMContentLoaded", function () {
    const cards = document.querySelectorAll(".card");
    const cards2 = document.querySelectorAll(".card2");

    cards.forEach(card => {
        new bootstrap.Tooltip(card);

        card.addEventListener("click", function () {
            const modal = new bootstrap.Modal(document.getElementById("reservationModal"));
            modal.show();
        });
    });

    cards2.forEach(card => {
        new bootstrap.Tooltip(card);

        card.addEventListener("click", function () {
            const modal = new bootstrap.Modal(document.getElementById("ModalReservadal"));
            modal.show();
        });
    });
});

