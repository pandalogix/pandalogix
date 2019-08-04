import React from "react";
import "./App.css";

import Sidebar from "./sidebar/Sidebar";
import { BrowserRouter as Router, Route } from "react-router-dom";

import Home from "../pages/home/Home";
import Dashboard from "../pages/dashboard/Dashboard";
import User from "../pages/user/User";
import History from "../pages/history/History";
import Pads from "../pages/pad/Pads";

import { library } from "@fortawesome/fontawesome-svg-core";
import {far} from '@fortawesome/free-regular-svg-icons';
import { fal } from "@fortawesome/pro-light-svg-icons";
library.add(far,fal);

function App() {
  return (
    <div className="App">
      <Router>
        <Sidebar />
        <div className="content">
          <Route path="/" exact component={Home} />
          <Route path="/dashboard" component={Dashboard} />
          <Route path="/user" component={User} />
          <Route path="/history" component={History} />
          <Route path="/pads" component={Pads} />
        </div>
      </Router>
    </div>
  );
}

export default App;
