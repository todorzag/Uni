import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import type { ComputerType } from "../../types/ComputerType";
import "./ComputersPage.css";
import "./ComputerDetailsPage.css";
import api from "../../api";

export default function ComputerDetailsPage() {
  const { id } = useParams();
  const [computer, setComputer] = useState<ComputerType>();
  const [error, setError] = useState("");
  const [address, setAddress] = useState("");
  const [showModal, setShowModal] = useState(false);
  const [orderMessage, setOrderMessage] = useState('');
  const navigate = useNavigate();

  useEffect(() => {
    const fetchComputer = async () => {
      try {
        const res = await api.get(`/computers/${id}`);
        setComputer(res.data);
      } catch {
        setError("Failed to load computer details.");
      }
    };

    fetchComputer();
  }, [id]);

  const handleOrder = async () => {
    if (!address) {
      setOrderMessage("Please enter a delivery address.");
      return;
    }

    if (!computer) {
      setOrderMessage("Computer details are not loaded.");
      return;
    }

    try {
      await api.post("/orders/create", {
        orderAddress: address,
        items: [{ computerId: computer.id, quantity: 1 }],
      });
      setOrderMessage("Order placed successfully.");
      setShowModal(false);
      navigate("/orders");
    } catch {
      setOrderMessage("Failed to place order.");
    }
  };

  if (error) return <div style={{ color: "red" }}>{error}</div>;
  if (!computer) return <div>Loading...</div>;

  return (
    <div className="computers-container">
      <div className="computer-details-card">
        <h2>{computer.name}</h2>
        <img
          src={`/assets/${computer.imageUrl}`}
          alt={computer.name}
          className="computer-image"
        />
        <p><strong>Price:</strong> ${computer.price}</p>
        <p><strong>Description:</strong> {computer.description}</p>
        <p><strong>CPU:</strong> {computer.processor}</p>
        <p><strong>RAM:</strong> {computer.ram} GB</p>
        <p><strong>Storage:</strong> {computer.storage} GB</p>
        <p><strong>Created At:</strong> {new Date(computer.createdAt).toLocaleString()}</p>

        <button onClick={() => setShowModal(true)} className="buy-button">Buy</button>
      </div>

      {showModal && (
        <div className="modal-overlay">
          <div className="modal-content">
            <h3>Enter Delivery Address</h3>
            <input
              type="text"
              value={address}
              onChange={(e) => setAddress(e.target.value)}
              placeholder="Address"
              className="modal-input"
            />
            <div className="modal-actions">
              <button onClick={handleOrder} className="confirm-button">Confirm Order</button>
              <button onClick={() => setShowModal(false)} className="cancel-button">Cancel</button>
            </div>
            {orderMessage && <p className="order-message">{orderMessage}</p>}
          </div>
        </div>
      )}
    </div>
  );
}
