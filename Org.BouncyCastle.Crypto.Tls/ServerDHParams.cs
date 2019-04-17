// ServerDHParams
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Tls;
using Org.BouncyCastle.Math;
using System;
using System.IO;

public class ServerDHParams
{
	protected readonly DHPublicKeyParameters mPublicKey;

	public virtual DHPublicKeyParameters PublicKey => mPublicKey;

	public ServerDHParams(DHPublicKeyParameters publicKey)
	{
		if (publicKey == null)
		{
			throw new ArgumentNullException("publicKey");
		}
		mPublicKey = publicKey;
	}

	public virtual void Encode(Stream output)
	{
		DHParameters parameters = mPublicKey.Parameters;
		BigInteger y = mPublicKey.Y;
		TlsDHUtilities.WriteDHParameter(parameters.P, output);
		TlsDHUtilities.WriteDHParameter(parameters.G, output);
		TlsDHUtilities.WriteDHParameter(y, output);
	}

	public static ServerDHParams Parse(Stream input)
	{
		BigInteger p = TlsDHUtilities.ReadDHParameter(input);
		BigInteger g = TlsDHUtilities.ReadDHParameter(input);
		BigInteger y = TlsDHUtilities.ReadDHParameter(input);
		return new ServerDHParams(TlsDHUtilities.ValidateDHPublicKey(new DHPublicKeyParameters(y, new DHParameters(p, g))));
	}
}