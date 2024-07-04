import { lazy } from "react";
import Loadable from "../components/shared/Loadable";
const Staff = Loadable(lazy(() => import('./staff/index')));


const Router = [
    { path: "/", element: <Staff /> },
]

export default Router