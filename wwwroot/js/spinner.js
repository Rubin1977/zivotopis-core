document.addEventListener('DOMContentLoaded', function () {
    const form = document.getElementById('contact-form');
    const spinner = document.getElementById('spinner');

    if (form && spinner) {
        form.addEventListener('submit', function () {
            spinner.style.display = 'block';
        });
    }
});
