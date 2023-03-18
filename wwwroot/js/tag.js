var counter = 0;
function addRow() {
    var table = document.getElementById("AddItemsTable");
    var row = table.insertRow(-1);
    var cell1 = row.insertCell(0);
    cell1.innerHTML = '<div class="col-12"><label class="form-label">Tag <span class="text-muted">(Optional)</span></label><input type="text" class="form-control" name="Tags[' + counter + '].Name" /></div>';
    counter++;
}