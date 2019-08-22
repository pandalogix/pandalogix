import React, { useState } from "react";
import { NavLink } from "react-router-dom";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";

import "./sidebar.css";

export default () => {
  const [open, setOpen] = useState(true);
  return (
    <nav className={open ? "" : "collapsed"}>
      <NavLink className="nav-item" activeClassName="active" exact to="/">
        <div>
          <FontAwesomeIcon icon={["fal", "home-alt"]} size="2x" fixedWidth />
        </div>
        {open && <span>Home</span>}
      </NavLink>
      <NavLink className="nav-item" activeClassName="active" to="/dashboard">
        <div>
          <FontAwesomeIcon icon={["fal", "tachometer"]} size="2x" fixedWidth />
        </div>
        {open && <span>Dashboard</span>}
      </NavLink>
      <NavLink className="nav-item" activeClassName="active" to="/user">
        <div>
          <FontAwesomeIcon icon={["fal", "user"]} size="2x" fixedWidth />
        </div>
        {open && <span>User</span>}
      </NavLink>
      <NavLink className="nav-item" activeClassName="active" to="/pads">
        <div>
          <FontAwesomeIcon icon={["fal", "clipboard"]} size="2x" fixedWidth />
        </div>
        {open && <span>Pads</span>}
      </NavLink>
      <NavLink className="nav-item" activeClassName="active" to="/history">
        <div>
          <FontAwesomeIcon icon={["fal", "history"]} size="2x" fixedWidth />
        </div>
        {open && <span>History</span>}
      </NavLink>
      <NavLink className="nav-item toggle" onClick={() => setOpen(!open)} to="">
        {open && (
          <FontAwesomeIcon icon={["fal", "angle-double-left"]} size="2x" fixedWidth/>
        )}
        {!open && (
          <FontAwesomeIcon icon={["fal", "angle-double-right"]} size="2x" fixedWidth/>
        )}
      </NavLink>
    </nav>
  );
};
