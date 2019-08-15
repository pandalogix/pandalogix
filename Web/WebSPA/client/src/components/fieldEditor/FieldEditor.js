import React, { useState } from "react";
import { ComboBoxComponent } from "@syncfusion/ej2-react-dropdowns";
import { SwitchComponent } from "@syncfusion/ej2-react-buttons";

import "./fieldeditor.css";
export default props => {
  const { field, preNodes } = props;
  const [fromNode, setFromNode] = useState(true);
  const [selectdNode, setSelectdNode] = useState(null);
  const fields = selectdNode
    ? selectdNode.data.fieldsMetaData.map(f => f.name)
    : [];

  return (
    <div className="form-group" id={field.name}>
      <div className="property-name">{field.name}</div>
      <SwitchComponent
        id={field.name}
        checked={fromNode}
        change={e => {
          setFromNode(!fromNode);
        }}
      />{" "}
      <label className="field-source" htmlFor={field.name}>{`Source  (${
        fromNode ? "Node" : "Constant"
      })`}</label>
      {fromNode && (
        <div>
          <ComboBoxComponent
            key="nodeselector"
            placeholder="select linked node"
            dataSource={preNodes.map(n => n.id)}
            change={arg => {
              const index = preNodes.findIndex(
                n => n.id === arg.itemData.value
              );
              if (index !== -1) {
                setSelectdNode(preNodes[index]);
              }
            }}
          />
          <ComboBoxComponent
            key="fieldselector"
            placeholder="select node field"
            dataSource={fields}
          />
        </div>
      )}
      {!fromNode && <input type="text" class="form-control" id={field.name} />}
    </div>
  );
};
