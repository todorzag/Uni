// src/pages/OrdersPage.tsx
import { useEffect, useState } from 'react';
import './OrdersPage.css';
import { FaCheckCircle, FaClock } from 'react-icons/fa';
import api from '../../api';

interface OrderItem {
  id: number;
  quantity: number;
  computerId: number;
}

interface Order {
  id: number;
  createdAt: string;
  orderAddress: string;
  items: OrderItem[];
  status: 'Pending' | 'Completed';
}

interface Computer {
  id: number;
  name: string;
  imageUrl: string;
  price: number;
}

export default function OrdersPage() {
  const [orders, setOrders] = useState<Order[]>([]);
  const [computers, setComputers] = useState<Computer[]>([]);
  const [error, setError] = useState('');
  const [sortByDate, setSortByDate] = useState<'asc' | 'desc'>('desc');

  useEffect(() => {
    const fetchData = async () => {
      try {
        const [ordersRes, computersRes] = await Promise.all([
          api.get('/orders/my'),
          api.get('/computers')
        ]);
        setOrders(ordersRes.data);
        setComputers(computersRes.data);
      } catch {
        setError('Failed to load your orders or computers.');
      }
    };

    fetchData();
  }, []);

  const getComputerById = (id: number) => computers.find(c => c.id === id);

  const sortedOrders = [...orders].sort((a, b) => {
    const dateA = new Date(a.createdAt).getTime();
    const dateB = new Date(b.createdAt).getTime();
    return sortByDate === 'asc' ? dateA - dateB : dateB - dateA;
  });

  return (
    <div className="orders-container">
      <h2>Your Orders</h2>
      <div className="sort-controls">
        <label htmlFor="sort-date">Sort by date:</label>
        <select
          id="sort-date"
          value={sortByDate}
          onChange={(e) => setSortByDate(e.target.value as 'asc' | 'desc')}
        >
          <option value="desc">Newest First</option>
          <option value="asc">Oldest First</option>
        </select>
      </div>
      {error && <p className="error-message">{error}</p>}
      {orders.length === 0 ? (
        <p className="no-orders">You have no orders yet.</p>
      ) : (
        sortedOrders.map(order => (
          <div key={order.id} className="order-card">
            <h3>Order #{order.id}</h3>
            <p><strong>Date:</strong> {new Date(order.createdAt).toLocaleString()}</p>
            <p><strong>Address:</strong> {order.orderAddress}</p>
            <p>
              <strong>Status:</strong>{' '}
              {order.status === 'Completed' ? (
                <span className="status completed">
                  <FaCheckCircle color="green" /> Completed
                </span>
              ) : (
                <span className="status pending">
                  <FaClock color="orange" /> Pending
                </span>
              )}
            </p>
            <div className="order-items">
              {order.items.map(item => {
                const computer = getComputerById(item.computerId);
                if (!computer) return null;
                return (
                  <div key={item.id} className="order-item">
                    <img src={`assets/${computer.imageUrl}`} alt={computer.name} className="order-item-image" />
                    <div className="order-item-details">
                      <p className="item-name">{computer.name}</p>
                      <p>Quantity: {item.quantity}</p>
                      <p>Price: ${computer.price}</p>
                    </div>
                  </div>
                );
              })}
            </div>
          </div>
        ))
      )}
    </div>
  );
}
