import React, { useState, useEffect } from "react";
import {
  SymbolPaletteComponent,
  NodeConstraints
} from "@syncfusion/ej2-react-diagrams";
import {dom} from '@fortawesome/fontawesome-svg-core'
import axios from "axios";

dom.watch();

const getSymboalContent = nodemetadata => {
  let iconortext = nodemetadata.name;
  if (nodemetadata.nodeIcon) {
    iconortext = `<i class="${nodemetadata.nodeIcon} fa-2x nodeicon"></i>`;
  }else{
    iconortext =`<span>${nodemetadata.name}</span>`
  }

  return `<div class="nodeWrapper">
            ${iconortext}
          </div>`;
};

const getPalleteSymbols = metadata => {
  let symbols = [];
  for (const attr in metadata) {
    if (metadata.hasOwnProperty(attr)) {
      const element = metadata[attr];
      symbols.push({
        id: attr,
        title:attr.toLocaleUpperCase(),
        expanded: true,
        symbols: element.map(n => {
          return {
            id: n.nodeData.name,
            data: n,
            shape: {
              type: "HTML",
              content: getSymboalContent(n.nodeData)
            },
            style:{
              width:'80px'
            }

          };
        })
      });
    }
  }
  return symbols;
};

export default () => {
  const [metadata, setMetadata] = useState([]);

  useEffect(() => {
    const fetchMetadata = async () => {
      const result = await axios.get(
        "http://localhost:5000/api/metadata/nodes"
      );
      const symbols = getPalleteSymbols(result.data);
      setMetadata(symbols);
    };
    fetchMetadata();
  }, []);
  return (
    <SymbolPaletteComponent
      id="palette"
      expandMode="Multiple"
      palettes={metadata}
      width="100%"
      symbolHeight={50}
      symbolWidth={50}
      symbolMargin={{ left: 5, right: 5, top: 5, bottom: 5 }}
      getSymbolInfo={symbol => {
        return { fit: true };
      }}
      getNodeDefaults={node => {
        node.width = 100;
        node.height = 100;
        node.constraints = NodeConstraints.AllowDrop | NodeConstraints.Default;
      }}
    />
  );
};
