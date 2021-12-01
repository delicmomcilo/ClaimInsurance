import { TextField, Button, Select, MenuItem } from "@mui/material";
import React from "react";
import "./App.css";
import { TypeEnum } from "./types/types";
import { useClaims } from "./utils/hooks/useClaims";
import { useClaimsForm } from "./utils/hooks/useClaimsForm";

const logo =
  "https://images.squarespace-cdn.com/content/v1/607d41cfe200d92857a50b86/1623869743579-SAILC0MMI1K9RYFGZ3Z8/instech_logo.png?format=1500w";

function App() {
  const { claims, deleteClaim, addClaim } = useClaims();
  const { claim, error, handleClaimChange, handleSubmit } = useClaimsForm(submitClaim);

  function submitClaim() {
    addClaim(claim);
  }

  return (
    <div className="App">
      <div className="header">
        {/* Oops, inline styling :S */}
        <div style={{ fontSize: "3rem", fontWeight: "bold" }}>Insurance Claim Handler</div>
        <img src={logo} />
      </div>
      <table style={{ padding: "3rem", textAlign: "center" }}>
        <thead>
          <tr>
            <th>Name</th>
            <th>Damage cost</th>
            <th>Year</th>
            <th>Type</th>
          </tr>
        </thead>
        <tbody>
          {claims.map((claim) => {
            return (
              <>
                <tr>
                  <td>{claim.name}</td>
                  <td>{claim.damageCost}</td>
                  <td>{claim.year}</td>
                  <td>{TypeEnum[claim.type]}</td>
                  <td>
                    <Button variant="outlined" color="error" onClick={() => deleteClaim(claim.id)}>
                      Delete
                    </Button>
                  </td>
                </tr>
              </>
            );
          })}
          <tr>
            <td>
              <TextField onChange={(e) => handleClaimChange(e)} value={claim.name} name="name" id="outlined-basic" variant="outlined" />
            </td>
            <td>
              <TextField
                onChange={(e) => handleClaimChange(e)}
                name="damageCost"
                value={claim.damageCost}
                id="outlined-basic"
                variant="outlined"
              />
            </td>
            <td>
              <TextField onChange={(e) => handleClaimChange(e)} name="year" value={claim.year} id="outlined-basic" variant="outlined" />
            </td>
            <td>
              <Select defaultValue={1} name="type" onChange={(e) => handleClaimChange(e)}>
                <MenuItem value={TypeEnum.Collision}>Collision</MenuItem>
                <MenuItem value={TypeEnum.Grounding}>Grounding</MenuItem>
                <MenuItem value={TypeEnum.BadWeather}>Bad Weather</MenuItem>
                <MenuItem value={TypeEnum.Fire}>Fire</MenuItem>
              </Select>
            </td>
            <td>
              <Button variant="contained" onClick={(e) => handleSubmit(e)}>
                Add
              </Button>
            </td>
          </tr>
          <tr>
            <td colSpan={100}>{error}</td>
          </tr>
        </tbody>
      </table>
    </div>
  );
}

export default App;
