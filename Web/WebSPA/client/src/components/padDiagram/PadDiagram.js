import React, { useState } from "react";

import {
  DiagramComponent,
  SnapConstraints,
  SelectorConstraints,
  PortVisibility,
  DiagramTools,
  NodeConstraints
} from "@syncfusion/ej2-react-diagrams";
import Property from "../property/Property";

import "./paddiagram.css";
import Palette from "../palette/Palette";
import { FetchData } from "../padDataSource/PadDataSource";

let diagramInstance;
const setNodeDefault = (node, id) => {
  node.constraints = NodeConstraints.Default | NodeConstraints.Shadow;
  node.ports = [
    {
      id: "port1",
      offset: { x: 0, y: 0.5 },
      visibility: PortVisibility.Hover,
      shape: "Circle"
    },
    {
      id: "port2",
      offset: { x: 1, y: 0.5 },
      visibility: PortVisibility.Hover,
      shape: "Circle"
    }
  ];
  node.data.nodeId = id;
};

const setConnectorDefault = connector => {
  connector.type = "Bezier";
  connector.style.strokeColor = "#6f409f";
  connector.style.strokeWidth = 2;
  connector.targetDecorator = {
    style: {
      strokeColor: "#6f409f",
      fill: "#6f409f"
    }
  };
};

const onCollectionChanged = arg => {
  if (arg.state === "Changed") {
    if (
      arg.element &&
      arg.element.type === "Bezier" &&
      (!arg.element.sourcePortID ||
        !arg.element.targetPortID ||
        arg.element.sourceID === arg.element.targetID)
    ) {
      setTimeout(() => {
        diagramInstance.remove(arg.element);
      }, 100);
    }
  }
};

const onMouseenter = arg => {
  if (arg.actualObject) {
    diagramInstance.tool = DiagramTools.DrawOnce;
  }
};

const onClick = arg => {
  diagramInstance.select(arg.element);
  diagramInstance.tool = DiagramTools.Default;
};

let data = FetchData();

const onMouserLeave = arg => {
  diagramInstance.tool = DiagramTools.Default;
};
export default () => {
  const [selectedNode, setSelectNode] = useState(null);

  return (
    <div className="diagram-main">
      <div className="palette-container">
        <Palette />
      </div>
      <div className="diagram-container">
        <DiagramComponent
          id="padDiagram"
          ref={d => (diagramInstance = d)}
          height="590px"
          snapSettings={{ constraints: SnapConstraints.ShowLines }}
          selectedItems={{
            constraints:
              SelectorConstraints.ConnectorSourceThumb |
              SelectorConstraints.ConnectorTargetThumb |
              SelectorConstraints.UserHandle |
              SelectorConstraints.ToolTip
          }}
          nodes={data.nodes}
          connectors={data.connectors}
          getNodeDefaults={node => {
            if (new Error().stack.indexOf("intDragStop") !== -1) {
              data.currentMaxSequenceId++;
            }
            setNodeDefault(node, data.currentMaxSequenceId);
          }}
          getConnectorDefaults={setConnectorDefault}
          tool={
            DiagramTools.MultipleSelect |
            DiagramTools.SingleSelect |
            DiagramTools.ZoomPan
          }
          drawingObject={{
            id: "connector",
            type: "Bezier"
            // constraints:
            //   ConnectorConstraints.Select |
            //   ConnectorConstraints.Delete |
            //   ConnectorConstraints.Interaction
          }}
          collectionChange={onCollectionChanged}
          mouseEnter={onMouseenter}
          mouseLeave={onMouserLeave}
          click={onClick}
          selectionChange={arg => {
            setSelectNode(arg.newValue);
          }}
        />
      </div>
      <div className="property-container">
        <Property node={selectedNode} />
      </div>

      <button
        onClick={() => {
          console.log(diagramInstance.nodes);
          console.log(diagramInstance.connectors);
        }}
      >
        SAVE
      </button>
    </div>
  );
};
