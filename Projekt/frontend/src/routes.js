/*!

=========================================================
* Black Dashboard React v1.1.0
=========================================================

* Product Page: https://www.creative-tim.com/product/black-dashboard-react
* Copyright 2020 Creative Tim (https://www.creative-tim.com)
* Licensed under MIT (https://github.com/creativetimofficial/black-dashboard-react/blob/master/LICENSE.md)

* Coded by Creative Tim

=========================================================

* The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

*/
import Dashboard from "views/Dashboard.jsx";
import AdminPanel from "views/AdminPanel.jsx"
import MovieDetails from "views/MovieDetails.jsx"


var routes = [
  {
    path: "/dashboard",
    name: "Strona główna",
    icon: "tim-icons icon-chart-pie-36",
    component: Dashboard,
    layout: "/main"
  },
  {
    path: "/admin-panel",
    name: "Panel administratora",
    icon: "tim-icons icon-badge",
    component: AdminPanel,
    layout: "/main"
  },
  {
    path: "/movie/:movieId",
    name: "Szczegóły",
    icon: "tim-icons icon-badge",
    component: MovieDetails,
    layout: "/main"
  },
  
];
export default routes;
