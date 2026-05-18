/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./Pages/**/*.{cshtml,razor}", "./Areas/Identity/Pages/**/*.{cshtml,razor}", "./wwwroot/js/**/*.js"],
    theme: {
        extend: {},
    },
    plugins: [
        require('@tailwindcss/forms'),
    ],
}