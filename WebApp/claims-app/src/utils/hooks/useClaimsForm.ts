import { useState } from "react";
import { ClaimsRequest } from "../../types/types";

export const useClaimsForm = (submit: () => void) => {

  const [claim, setClaim] = useState<ClaimsRequest>({
    year: 2020,
    name: "",
    damageCost: 10,
    type: 1,
  });
  
  const [error, setError] = useState("");


  const handleSubmit = (event: React.FormEvent<EventTarget>) => {
    var currentYear = new Date().getFullYear();
    if(claim.damageCost > 100 || claim.name.length == 0 || (claim.year > currentYear || claim.year < (currentYear - 10))){
      setError("Vennligst  fyll inn skjemaet på riktig format");
    } else {
      setError("")
      submit();
    }
  };

  const handleClaimChange = (event: any) => {
    let { name, value } = event.target;

    setClaim({
      ...claim,
      [name]:
        typeof claim[name as keyof ClaimsRequest] === "number"
          ? value === ""
            ? 0
            : parseInt(value)
          : value,
    });
  };

  return { claim, error, handleClaimChange, handleSubmit };
};
