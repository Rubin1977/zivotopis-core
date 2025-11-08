function updateDateTime() {
    const now = new Date();

    const dateString = now.toLocaleDateString('sk-SK', {
        weekday: 'long',
        year: 'numeric',
        month: 'long',
        day: 'numeric'
    });

    const timeString = now.toLocaleTimeString('sk-SK', {
        hour: '2-digit',
        minute: '2-digit',
        second: '2-digit'
    });

    document.getElementById('current-time').textContent = `Presný čas: ${timeString} | ${dateString}`;
}

// Aktualizuj každú sekundu
setInterval(updateDateTime, 1000);

// Spusti hneď po načítaní
updateDateTime();
