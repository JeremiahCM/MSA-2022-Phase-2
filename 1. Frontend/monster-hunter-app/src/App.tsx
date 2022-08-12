import axios from 'axios';
import { useState } from 'react';
import Box from "@mui/material/Box";
import Container from '@mui/material/Container';
import Divider from '@mui/material/Divider';
import IconButton from "@mui/material/IconButton";
import SearchIcon from "@mui/icons-material/Search";
import TextField from "@mui/material/TextField";

import './App.css';
interface ILocation {
  id: number;
  zoneCount: number;
  name: string;
}

function App() {
  // State variables
  const [monsterName, setMonsterName] = useState("");
  const [monsterData, setMonsterData] = useState<null | undefined | any>(undefined);

  const MONSTER_HUNTER_BASE_URL = "https://mhw-db.com"

  return (
      <div>
        <Box className="search-field">
          <h1 id="search-header">
            Monster Hunter World: Monster Search
          </h1>

          <TextField
            id="search-bar"
            className="text"
            value={monsterName}
            onChange={(prop: any) => {
              setMonsterName(prop.target.value);
            }}
            label="Enter a Monster Name..."
            variant="outlined"
            placeholder="e.g. Anjanath..."
            size="small"
          />
          <IconButton
            aria-label="search"
            onClick={() => {
              search();
            }}
          >
            <SearchIcon style={{ fill: "blue" }} />
          </IconButton>
        </Box>

        {/* Display Monster Data if it exists */}
        {monsterData === undefined ? (
          <Box className="monster-unknown">
              <p>Monster not found</p>
          </Box>
        ) : (
          <Box className="monster-result">
            <Container className="monster-header">
            <h1 style={{textAlign: "center"}}>{monsterData.name}</h1>
            <h3 style={{
              textAlign: "center",
              textTransform: "capitalize"
            }}>
              {monsterData.species}
            </h3>
            </Container>
            <Divider variant="middle" />
            <p><b>Description: </b>{monsterData.description}</p>
            <p><b>Locations: </b></p>
            <p> - {getLocations()?.join(', ')}</p>
            <p><b>Elements: </b></p>
            <p> - {getElements()?.join(', ')}</p>
          </Box>
        )}
      </div>
  );

  // Monster Hunter API calling function
  function search(){
      axios.get(MONSTER_HUNTER_BASE_URL + "/monsters?q={\"name\":\"" + monsterName + "\"}")
      .then((res) => {
        setMonsterData(res.data[0]);
      })
  }

  function getLocations() {
    if (monsterData !== undefined && monsterData !== null) {
      return monsterData.locations.map((location: ILocation) => `${location.name} (Zone ${location.zoneCount})`);
    }
  }

  function getElements() {
    if (monsterData !== undefined && monsterData !== null) {
      return monsterData.elements.map((element: string) => element.charAt(0).toUpperCase() + element.slice(1));
    }
  }
}

export default App;