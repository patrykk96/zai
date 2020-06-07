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
import AdminPanel from "views/AdminPanel.jsx";
import MovieDetails from "views/MovieDetails.jsx";
import ReviewAdd from "components/Reviews/ReviewAdd";
import ReviewUpdate from "components/Reviews/ReviewUpdate";
import FavouriteMovies from "views/FavouriteMovies";
import ReviewDetails from "views/ReviewDetails";
import MovieReviews from "views/MovieReviews";

var routes = [
  {
    path: "/dashboard",
    name: "Strona główna",
    icon: "tim-icons icon-chart-pie-36",
    component: Dashboard,
    layout: "/main",
  },
  {
    path: "/admin-panel",
    name: "Panel administratora",
    icon: "tim-icons icon-badge",
    component: AdminPanel,
    layout: "/main",
  },
  {
    path: "/movie/:movieId",
    name: "Szczegóły",
    icon: "tim-icons icon-badge",
    component: MovieDetails,
    layout: "/main",
  },
  {
    path: "/reviewAdd/:movieId",
    name: "Dodaj recenzję",
    icon: "tim-icons icon-badge",
    component: ReviewAdd,
    layout: "/main",
  },
  {
    path: "/reviewUpdate/:reviewId",
    name: "Zaktualizuj recenzję",
    icon: "tim-icons icon-badge",
    component: ReviewUpdate,
    layout: "/main",
  },
  {
    path: "/favouriteMovies",
    name: "Twoje ulubione filmy",
    icon: "tim-icons icon-badge",
    component: FavouriteMovies,
    layout: "/main",
  },
  {
    path: "/review/:reviewId",
    name: "Szczegóły",
    icon: "tim-icons icon-badge",
    component: ReviewDetails,
    layout: "/main",
  },
  {
    path: "/reviews/:movieId",
    name: "Recenzje",
    icon: "tim-icons icon-badge",
    component: MovieReviews,
    layout: "/main",
  },
  {
    path: "/userReviews",
    name: "Recenzje",
    icon: "tim-icons icon-badge",
    component: MovieReviews,
    layout: "/main",
  },
];
export default routes;
