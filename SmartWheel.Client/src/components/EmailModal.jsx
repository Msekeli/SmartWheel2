import { useState } from "react";

function EmailModal({ onSubmit, onClose, isLoading }) {
  const [email, setEmail] = useState("");

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!email) return;
    onSubmit(email);
  };

  return (
    <div className="modal-overlay">
      <div className="modal-card">
        <div className="modal-title">Enter Your Email</div>

        <form onSubmit={handleSubmit}>
          <input
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="example@email.com"
            required
          />

          <button type="submit" disabled={isLoading}>
            {isLoading ? "Checking..." : "Continue"}
          </button>
        </form>

        <button onClick={onClose}>Cancel</button>
      </div>
    </div>
  );
}

export default EmailModal;
