/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ["./**/*.{razor,html,cshtml}"],
  theme: {
    extend: {},
    fontFamily: {
      'mono': ['"Fira Code"', 'ui-monospace', 'SFMono-Regular'],
      'body': ['"Open mono"']
    }
  },
  plugins: [],
}