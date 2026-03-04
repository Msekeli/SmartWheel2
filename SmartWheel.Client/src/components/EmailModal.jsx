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
      <div className="modal-card email-modal">
        <h2 className="modal-title">🎯 Spin the Smart Wheel</h2>

        <p className="modal-description">
          Enter your email to start the challenge.
        </p>

        <form onSubmit={handleSubmit} className="modal-form">
          <input
            className="modal-input"
            type="email"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            placeholder="example@email.com"
            required
          />

          <button
            className="modal-primary-button"
            type="submit"
            disabled={isLoading}
          >
            {isLoading ? "Checking..." : "Continue"}
          </button>
        </form>

        <button className="modal-cancel-button" onClick={onClose}>
          Cancel
        </button>
      </div>
    </div>
  );
}

export default EmailModal;
