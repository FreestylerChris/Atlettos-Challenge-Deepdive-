const btn = document.getElementById('btn');
const count = document.getElementById('count');

// Event listener for hover effect on button
btn.addEventListener('mouseenter', () => {
    count.classList.remove('text-accent');
    count.classList.add('text-primary-100', 'bg-accent', 'shadow-md');
    console.log('Adding bounce animation');
    console.log('Classes after adding:', count.classList.toString());
});

btn.addEventListener('mouseleave', () => {
    count.classList.remove('text-primary-100', 'bg-accent', 'shadow-md');
    count.classList.add('text-accent');
});

