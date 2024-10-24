//function showContent(section) {

//    if (content === 'dashboard-admin') 
//        window.location.href = "/Admin/Dashboard";
//    }
//}

function showContent(contentId) {
    const contents = document.querySelectorAll('.content');

    // Hide all contents and remove 'active' class
    contents.forEach(content => {
        content.classList.remove('active');
    });

    // Show the selected content
    const targetContent = document.getElementById(contentId);
    targetContent.classList.add('active');
}


function toggleView() {
    const container = document.getElementById('file-container');
    const toggleBtn = document.getElementById('toggleView');
    const fileNames = document.querySelectorAll('.file-name');
    const cards = document.querySelectorAll('.card-body');

    // Jika saat ini dalam tampilan grid, ubah ke list
    if (container.classList.contains('row-cols-md-6')) {
        container.classList.remove('row-cols-md-6');
        container.classList.add('list-view');
        toggleBtn.textContent = 'Grid';  // Teks tombol berubah ke 'Grid'

        // Pindahkan nama file ke dalam card-body dengan data-file-name
        fileNames.forEach((fileName, index) => {
            const fileText = fileName.textContent;
            cards[index].setAttribute('data-file-name', fileText);  // Simpan nama file di atribut
            fileName.style.display = 'none'; // Sembunyikan nama di luar card
        });

    } else {
        // Jika saat ini dalam tampilan list, ubah ke grid
        container.classList.remove('list-view');
        container.classList.add('row-cols-md-6');
        toggleBtn.textContent = 'List';  // Teks tombol berubah ke 'List'

        // Tampilkan kembali nama file di luar card dan hapus dari dalam card-body
        fileNames.forEach((fileName, index) => {
            fileName.style.display = 'block'; // Tampilkan kembali nama file di luar
            cards[index].removeAttribute('data-file-name'); // Hapus atribut nama file
        });
    }
}


function loadContent(url) {
    $.ajax({
        url: url,
        type: 'GET',
        success: function (data) {
            $('#main-content').html(data);  // Replace content with the new content
        },
        error: function () {
            alert('Error loading content');
        }
    });
}

// Load Dashboard content by default when page is first loaded
$(document).ready(function () {
    console.log("Document ready - Loading dashboard content...");  // Debug log
    loadContent('@Url.Action("Dashboard", "Home")');
});

function showDetails(name, createdDate, createdBy, lastModified) {
    // Update sidebar content with file details
    document.getElementById('doc-name').textContent = name;
    document.getElementById('created-by').textContent = createdBy;
    document.getElementById('edited-by').textContent = createdBy;
    document.getElementById('modified-date').textContent = lastModified;

    // Show the sidebar
    document.getElementById('right-sidebar').classList.add('open');
    document.getElementById('main-content').classList.add('with-sidebar');
}

function hideDetails() {
    document.getElementById('right-sidebar').classList.remove('open');
    document.getElementById('main-content').classList.remove('with-sidebar');
}

// Hide the sidebar when clicking outside of it
document.addEventListener('click', function (event) {
    const sidebar = document.getElementById('right-sidebar');
    const mainContent = document.getElementById('main-content');

    // Check if the click is outside the sidebar and file items
    if (!sidebar.contains(event.target) && !mainContent.contains(event.target)) {
        hideDetails();
    }
});

// Mendapatkan elemen dropZone dan input file
const dropZone = document.getElementById('dropZone');
const fileInput = document.getElementById('DocumentItem');

// Prevent default behavior (Prevent file from being opened)
['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
    dropZone.addEventListener(eventName, preventDefaults, false);
});

function preventDefaults(e) {
    e.preventDefault();
    e.stopPropagation();
}

// Highlight area when file is dragged over
['dragenter', 'dragover'].forEach(eventName => {
    dropZone.addEventListener(eventName, () => dropZone.classList.add('highlight'), false);
});

['dragleave', 'drop'].forEach(eventName => {
    dropZone.addEventListener(eventName, () => dropZone.classList.remove('highlight'), false);
});

// Handle dropped files
dropZone.addEventListener('drop', (e) => {
    const files = e.dataTransfer.files;
    fileInput.files = files; // Assign dropped file to input element
});


//sidebar kanan
function showDocumentDetail(documentId) {
    $.ajax({
        url: '/Document/GetDocumentDetail',
        type: 'GET',
        data: { id: documentId },
        success: function (result) {
            // Display document details in the sidebar
            $('#documentDetailContent').html(`
                <h3>${result.name}</h3>
                <p><strong>Tag:</strong> ${result.tag}</p>
                <p><strong>Released Date:</strong> ${result.releasedDate}</p>
            `);

            // Open sidebar
            $('#documentDetailSidebar').addClass('open');
            $('#documentContainer').addClass('sidebar-open');
        },
        error: function (xhr, status, error) {
            console.error("Error loading document details:", error);
        }
    });
}

function closeSidebar() {
    // Close sidebar
    $('#documentDetailSidebar').removeClass('open');
    $('#documentContainer').removeClass('sidebar-open');
}



