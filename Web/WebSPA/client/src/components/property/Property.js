import React from "react";
import { Node } from "@syncfusion/ej2-diagrams";
import "./property.css";
export default props => {
  const { node } = props;
  if (node instanceof Node) {
    return (
      <div className="card">
        <div className="header card-title">properties</div>
        <form className="property-content card-body">
          {node.data.fieldsMetaData &&
            node.data.fieldsMetaData.map(p => {
              return (
                <div className="form-group">
                  <label for={p.name}>{p.name}</label>
                  <input type="text" class="form-control" id={p.name} />
                </div>
              );
            })}
        </form>
        <div>
          {/* {node && (
            <pre>{JSON.stringify(node.data.fieldsMetaData, null, 2)}</pre>
          )} */}
        </div>
      </div>
    );
  } else {
    //if (node instanceof Connector) {
    return <></>;
  }
};
