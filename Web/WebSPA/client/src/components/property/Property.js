import React from "react";
import { Node } from "@syncfusion/ej2-diagrams";
import { SwitchComponent } from "@syncfusion/ej2-react-buttons";

import FieldEditor from "../fieldEditor/FieldEditor";
import "./property.css";
export default props => {
  const { node, preNodes, diagram } = props;
  let nodes = [];
  if (preNodes) nodes = preNodes.map(m => diagram.getObject(m));
  if (node instanceof Node) {
    let noTrigger = diagram.nodes.reduce((acc, current) => {
      if (!acc) return false;
      if (current.data.isTrigger) return !current.data.isTrigger;
      return true;
    }, true);
    return (
      <div className="card">
        <div className="header card-title">
          <h4>properties</h4>
          <p>{node.id}</p>
        </div>

        <form className="property-content card-body" key={node.id}>
          <div className="form-group">
            <SwitchComponent
              id="isInput"
              checked={node.data.isInput}
              change={e => {
                node.data.isInput = e.checked;
              }}
            />{" "}
            <label className="field-source" htmlFor="isInput">
              Input Node
            </label>
          </div>
          <div className="form-group">
            <SwitchComponent
              id="isTrigger"
              checked={node.data.isTrigger}
              disabled={!noTrigger && !node.data.isTrigger}
              change={e => {
                  node.data.isTrigger = e.checked;
                  if(e.checked){

                  }
              }}
            />{" "}
            <label className="field-source" htmlFor="isTrigger">
              Trigger Node
            </label>
          </div>
          {node.data.fieldsMetaData &&
            node.data.fieldsMetaData.map(p => {
              return (
                <FieldEditor
                  key={node.id + "_" + p.name}
                  field={p}
                  preNodes={nodes}
                />
              );
            })}
        </form>
        {/* <div>
          {JSON.stringify(preNodes,null,2)}
        </div> */}
      </div>
    );
  } else {
    //if (node instanceof Connector) {
    return <></>;
  }
};
