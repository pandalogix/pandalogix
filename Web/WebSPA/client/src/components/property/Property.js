import React from "react";
import { Node } from "@syncfusion/ej2-diagrams";
import FieldEditor from "../fieldEditor/FieldEditor";
import "./property.css";
export default props => {
  const { node, preNodes, diagram } = props;
  let nodes = [];
  if (preNodes) nodes = preNodes.map(m => diagram.getObject(m));
  if (node instanceof Node) {
    return (
      <div className="card">
        <div className="header card-title">properties</div>
        <form className="property-content card-body">
          {node.data.fieldsMetaData &&
            node.data.fieldsMetaData.map(p => {
              return <FieldEditor key={p} field={p} preNodes={nodes} />;
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
