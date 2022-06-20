
var takeDate = document.getElementById("TakenDate");
var returnDate = document.getElementById("ReturnDate");

takeDate.onchange = function () {
    returnDate.setAttribute("min", takeDate.value);
    returnDate.value = takeDate.value;
};
