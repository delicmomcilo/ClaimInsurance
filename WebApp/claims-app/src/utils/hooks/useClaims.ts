import { useEffect, useState } from "react";
import { ClaimResponse, ClaimsRequest } from "../../types/types";

export const useClaims = () => {
  const [claims, setClaims] = useState<ClaimResponse[]>([]);

  const fetchClaims = async () => {
    fetch("https://claimhandlingapi.azurewebsites.net/api/claim")
      .then((response) => response.json())
      .then((claims) => setClaims(claims))
      .catch((e) => console.log(e));
  };

  const deleteClaim = async (id: string) => {
    fetch(`https://claimhandlingapi.azurewebsites.net/api/claim/${id}`, {
      method: "DELETE",
    })
      .then(() => setClaims(claims.filter((claim) => claim.id !== id)))
      .catch((e) => console.log(e));
  };

  const addClaim = async (claim: ClaimsRequest) => {
    fetch("https://claimhandlingapi.azurewebsites.net/api/claim", {
      method: "POST",
      body: JSON.stringify(claim),
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    })
      .then((response) => response.json())
      .then((claim) => setClaims((oldClaims) => [...oldClaims, claim]))
      .catch((e) => console.log(e));
  };

  useEffect(() => {
    fetchClaims();
    return () => {};
  }, []);

  return { claims, deleteClaim, addClaim };
};
