import { useEffect, useState } from "react";
import type { ComputerType } from "../../types/ComputerType";
import api from "../../api";
import "./ComputersPage.css";
import { useNavigate } from "react-router-dom";

export default function ComputersPage() {
  const [computers, setComputers] = useState<ComputerType[]>([]);
  const [searchName, setSearchName] = useState("");
  const [minPrice, setMinPrice] = useState<number | null>(null);
  const [maxPrice, setMaxPrice] = useState<number | null>(null);
  const [errorMessage, setErrorMessage] = useState('');
  const navigate = useNavigate();

  const validateFilters = () => {
    if (minPrice && maxPrice && (minPrice > maxPrice || maxPrice < minPrice)) {
      setErrorMessage("Minimum price cannot be greater than maximum price.");
      return false;
    } 

    setErrorMessage('');
    return true;
  };

  const resetFilters = () => {
    setSearchName("");
    setMinPrice(null);
    setMaxPrice(null);
    fetchComputers("", null, null);
  };

  const fetchComputers = async (
    name = searchName,
    min = minPrice,
    max = maxPrice
  ) => {
    if (validateFilters()) {
      try {
        const res = await api.post("/computers/filter", {
          name: name,
          minPrice: min,
          maxPrice: max,
        });
        setComputers(res.data);
      } catch (err) {
        console.error("Error fetching computers:", err);
      }
    }
  };

  useEffect(() => {
    api.get("/computers").then((res) => setComputers(res.data));
  }, []);

  return (
    <div className="computers-container">
      <h2>Available Computers</h2>
      <div className="search-controls">
        <input
          placeholder="Search by name"
          value={searchName}
          onChange={(e) => setSearchName(e.target.value)}
        />
        <input
          placeholder="Min price"
          type="number"
          value={minPrice ?? ""}
          onChange={(e) => setMinPrice(parseFloat(e.target.value))}
        />
        <input
          placeholder="Max price"
          type="number"
          value={maxPrice ?? ""}
          onChange={(e) => setMaxPrice(parseFloat(e.target.value))}
        />
        <button onClick={() => fetchComputers(searchName, minPrice, maxPrice)}>
          Search
        </button>
        <button onClick={resetFilters}>Reset</button>
        {errorMessage && (
          <div className="errorMessage">{errorMessage}</div>
        )}
      </div>
      <div className="computers-grid">
        {computers.map((computer) => (
          <div key={computer.id} className="computer-card">
            <img
              src={`/assets/${computer.imageUrl}`}
              alt={computer.name}
              className="computer-image"
            />
            <h4>{computer.name}</h4>
            <p>{computer.type}</p>
            <p>{computer.description}</p>
            <p>${computer.price}</p>
            <button onClick={() => navigate(`/computers/${computer.id}`)}>See More Information</button>
          </div>
        ))}
      </div>
    </div>
  );
}
