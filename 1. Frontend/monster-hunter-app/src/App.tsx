import axios from 'axios';
import { useState } from 'react';
import { palette } from '@mui/system';
import Box from "@mui/material/Box";
import IconButton from "@mui/material/IconButton";
import Paper from "@mui/material/Paper";
import SearchIcon from "@mui/icons-material/Search";
import TextField from "@mui/material/TextField";
import './App.css';
import { transform } from 'typescript';

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
            <h1 style={{textAlign: "center"}}>{monsterData.name}</h1>
            <h3 style={{
              textAlign: "center",
              textTransform: "capitalize"
            }}>
              {monsterData.species}
            </h3>
            <p>Description: {monsterData.description}</p>
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
}

export default App;