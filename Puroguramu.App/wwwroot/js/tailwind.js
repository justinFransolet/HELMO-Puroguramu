// Sélectionner le bouton du menu utilisateur
const userMenuButton = document.getElementById('user-menu-button');
const dropdownMenu = document.getElementById('dropdown-menu');

// Cacher le menu utilisateur au chargement de la page
dropdownMenu.classList.add('hidden');

// Ajouter un gestionnaire d'événements au clic sur le bouton du menu utilisateur
userMenuButton.addEventListener('click', () => {

    if(dropdownMenu.classList.contains('hidden')) {
        dropdownMenu.classList.remove('hidden');
        dropdownMenu.classList.add('block');
    } else {
        dropdownMenu.classList.remove('block');
        dropdownMenu.classList.add('hidden');
    }
});

// Récupérer les liens
var indexLink = document.getElementById("index-link");
var dashboardLink = document.getElementById("dashboard-link");
var teacherDashboardLink = document.getElementById("teacher-dashboard-link");
var profileLink = document.getElementById("profile-link");

// Fonction pour mettre en surbrillance le lien actif
function highlightActiveLink(activeLink) {
    // Réinitialiser le style des liens
    indexLink.classList.remove("bg-gray-800", "text-white");
    indexLink.classList.add("text-gray-300", "hover:bg-gray-600", "hover:text-white");
    dashboardLink.classList.remove("bg-gray-800", "text-white");
    dashboardLink.classList.add("text-gray-300", "hover:bg-gray-600", "hover:text-white");
    profileLink.classList.remove("bg-gray-800", "text-white");
    profileLink.classList.add("text-gray-300", "hover:bg-gray-600", "hover:text-white");
    if(teacherDashboardLink) {
        teacherDashboardLink.classList.remove("bg-gray-800", "text-white");
        teacherDashboardLink.classList.add("text-gray-300", "hover:bg-gray-600", "hover:text-white");
    }
    

    // Mettre en surbrillance le lien actif
    activeLink.classList.remove("text-gray-300", "hover:bg-gray-600", "hover:text-white");
    activeLink.classList.add("bg-gray-800", "text-white");
}

// Fonction pour récupérer le lien actif depuis le stockage local
function getActiveLinkFromLocalStorage() {
    return localStorage.getItem("activeLink");
}

// Fonction pour enregistrer le lien actif dans le stockage local
function setActiveLinkToLocalStorage(activeLinkId) {
    localStorage.setItem("activeLink", activeLinkId);
}

// Déterminer quel lien est actif en fonction de ce qui est stocké dans le stockage local
document.addEventListener("DOMContentLoaded", function() {
    var activeLinkId = getActiveLinkFromLocalStorage();
    if (!activeLinkId) {
        activeLinkId = "index-link"; // Définir le lien "Accueil" comme actif par défaut
        setActiveLinkToLocalStorage(activeLinkId); // Enregistrer le lien actif dans le stockage local
    }
    var activeLink = document.getElementById(activeLinkId);
    highlightActiveLink(activeLink);
});

// Ajouter un gestionnaire d'événements de clic à chaque lien
indexLink.addEventListener("click", function(event) {
    event.preventDefault(); // Empêcher le comportement par défaut du lien
    highlightActiveLink(indexLink); // Mettre en surbrillance le lien Accueil
    setActiveLinkToLocalStorage("index-link"); // Enregistrer le lien actif dans le stockage local
    // Naviguer vers la page Accueil
    window.location.href = indexLink.getAttribute("href");
});

dashboardLink.addEventListener("click", function(event) {
    event.preventDefault(); // Empêcher le comportement par défaut du lien
    highlightActiveLink(dashboardLink); // Mettre en surbrillance le lien Dashboard
    setActiveLinkToLocalStorage("dashboard-link"); // Enregistrer le lien actif dans le stockage local
    // Naviguer vers la page Dashboard
    window.location.href = dashboardLink.getAttribute("href");
});

profileLink.addEventListener("click", function(event) {
    event.preventDefault(); // Empêcher le comportement par défaut du lien
    highlightActiveLink(profileLink); // Mettre en surbrillance le lien Profile
    setActiveLinkToLocalStorage("profile-link"); // Enregistrer le lien actif dans le stockage local
    // Naviguer vers la page Profile
    window.location.href = profileLink.getAttribute("href");
});
