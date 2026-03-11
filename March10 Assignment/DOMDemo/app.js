document.addEventListener("DOMContentLoaded", function(){

// select movie list
const list = document.querySelector('#movie-list ul');

// DELETE MOVIE
list.addEventListener('click', function(e){

    if(e.target.classList.contains('delete')){

        const li = e.target.parentElement;
        li.remove();
    }
});





// ADD MOVIE
const addForm = document.getElementById('add-movie');

addForm.addEventListener('submit', function(e){

    e.preventDefault();

    const input = addForm.querySelector('input');
    const value = input.value;

    // create elements
    const li = document.createElement('li');
    const movieName = document.createElement('span');
    const deleteBtn = document.createElement('span');

    // add text
    movieName.textContent = value;
    deleteBtn.textContent = "delete";

    
    // add classes
    movieName.classList.add('name');
    deleteBtn.classList.add('delete');

    // append elements
    li.appendChild(movieName);
    li.appendChild(deleteBtn);
    list.appendChild(li);

    // clear input
    input.value = "";

});

});