function confirmDelete(supplierSupplierId) {
    if (confirm("Are you sure you want to delete this supplier")) {
        window.location.href = "/SuppliersPages/" + supplierSupplierId + "/Delete"
    }
}